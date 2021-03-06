using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using Sitecore.SecurityModel.License;
using Sitecore.XA.Foundation.Theming.Bundler;
using Sitecore.XA.Foundation.Theming.Configuration;
using Sitecore.XA.Foundation.Theming.EventHandlers;
using Sitecore.XA.Foundation.Theming.Extensions;
using Sitecore.XA.Foundation.Theming.Pipelines.AssetService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlphaCentauri.XA.Foundation.Theming.Bundler
{
    public class AssetLinksGenerator : Sitecore.XA.Foundation.Theming.Bundler.AssetLinksGenerator
    {
        private readonly AssetConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        public AssetLinksGenerator()
        {
            _configuration = AssetConfigurationReader.Read();
        }

        /// <summary>
        ///  Static version of <see cref="GenerateAssetLinks"/>.
        /// </summary>
        /// <param name="themesProvider">The current site theme.</param>
        /// <returns></returns>
        public new static AssetLinks GenerateLinks(IThemesProvider themesProvider)
        {
            if (AssetContentRefresher.IsPublishing() || IsAddingRendering())
            {
                return new AssetLinks();
            }

            return (new AssetLinksGenerator()).GenerateAssetLinks(themesProvider);
        }

        /// <summary>
        /// Generate links for both styles and scripts.
        /// </summary>
        /// <param name="themesProvider">The current site theme.</param>
        /// <returns></returns>
        public override AssetLinks GenerateAssetLinks(IThemesProvider themesProvider)
        {
            // Validates SXA license.
            if (!License.HasModule("Sitecore.SXA"))
            {
                HttpContext.Current.Response.Redirect(Settings.NoLicenseUrl + "?license=Sitecore.SXA");

                return null;
            }

            var str = $"{Context.Item.ID}#{Context.Device.ID}#{Context.Database.Name}#{_configuration.RequestAssetsOptimizationDisabled}";

            var cache = HttpContext.Current.Cache[str];

            string cacheKey;

            if (cache != null && HttpContext.Current.Cache[cacheKey = GenerateCacheKey((int)cache)] != null)
            {
                return HttpContext.Current.Cache[cacheKey] as AssetLinks;
            }

            var assetsArgs = new AssetsArgs();

            CorePipeline.Run("assetService", assetsArgs);

            var hashCode = assetsArgs.GetHashCode();

            cacheKey = GenerateCacheKey(hashCode);

            if (!(HttpContext.Current.Cache[cacheKey] is AssetLinks result) || _configuration.RequestAssetsOptimizationDisabled)
            {
                result = new AssetLinks();

                if (!assetsArgs.AssetsList.Any())
                {
                    return result;
                }

                assetsArgs.AssetsList = assetsArgs.AssetsList.OrderBy(a => a.SortOrder).ToList();

                foreach (var assets in assetsArgs.AssetsList)
                {
                    switch (assets)
                    {
                        case ThemeInclude include:
                            AddThemeInclude(include, result, themesProvider);
                            continue;
                        case UrlInclude include:
                            AddUrlInclude(include, result);
                            continue;
                        case PlainInclude include:
                            AddPlainInclude(include, result);
                            continue;
                        default:
                            continue;
                    }
                }

                CacheLinks(cacheKey, result, DatabaseRepository.GetContentDatabase().Name.ToLowerInvariant().Equals("master", StringComparison.Ordinal) ?
                    AssetContentRefresher.MasterCacheDependencyKeys :
                    AssetContentRefresher.WebCacheDependencyKeys);
            }

            CacheHash(str, hashCode);

            return result;
        }

        /// <summary>
        /// This method overrides the original one in order to include Context Item in the cache key
        /// so no all the pages shared the same cached content.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        protected override string GenerateCacheKey(int hash)
        {
            var hostName = this.Context.Site.HostName;
            
            return $"{DatabaseRepository.GetContentDatabase().Name}#{Context.User.IsAuthenticated}#{_configuration.CacheKey}#{Sitecore.Context.Item.ID}#{hostName}#{hash}";
        }

        /// <summary>
        /// Populate AssetLinks with assets selected as base themes
        /// and appends the current theme at the end.
        /// </summary>
        /// <param name="themeInclude"></param>
        /// <param name="result"></param>
        /// <param name="themesProvider"></param>
        protected override void AddThemeInclude(ThemeInclude themeInclude, AssetLinks result, IThemesProvider themesProvider)
        {
            var theme = themeInclude.Theme;

            if (theme == null && !themeInclude.ThemeId.IsNull)
            {
                theme = ContentRepository.GetItem(themeInclude.ThemeId);
            }

            if (theme == null)
            {
                return;
            }

            Log.Debug($"Starting optimized files generation process for {theme.Name} with following configuration {_configuration}");

            var allThemes = GetAllThemes(theme);

            GetLinks(allThemes.FilterBaseThemes(), _configuration.ScriptsMode, _configuration.StylesMode, result);

            GetLinks(themesProvider.GetThemes(theme, allThemes), _configuration.ScriptsMode, _configuration.StylesMode, result);
        }

        /// <summary>
        /// Add plain data in AssetLinks
        /// </summary>
        /// <param name="plainInclude"></param>
        /// <param name="result"></param>
        protected override void AddPlainInclude(PlainInclude plainInclude, AssetLinks result)
        {
            if (plainInclude.Type == AssetType.Script)
            {
                result.Scripts.Add(plainInclude.Content);
            }
            else
            {
                result.Styles.Add(plainInclude.Content);
            }
        }

        /// <summary>
        /// Add HTML tags in AssetLinks
        /// </summary>
        /// <param name="urlInclude"></param>
        /// <param name="result"></param>
        protected override void AddUrlInclude(UrlInclude urlInclude, AssetLinks result)
        {
            if (urlInclude.Type == AssetType.Script)
            {
                result.Scripts.Add("<script src=\"" + urlInclude.Url + "\"></script>");
            }
            else
            {
                result.Styles.Add("<link href=\"" + urlInclude.Url + "\" rel=\"stylesheet\" />");
            }
        }

        /// <summary>
        /// Get base themes + current one.
        /// </summary>
        /// <param name="themeItem"></param>
        /// <returns></returns>
        protected IList<Item> GetAllThemes(Item themeItem)
            => GetThemesWithBaseThemes(themeItem, new List<ID>(), Templates.Theme.Fields.BaseLayout, true);

        /// <summary>
        /// Get base themes + current one.
        /// </summary>
        /// <param name="themeItem"></param>
        /// <param name="processedThemes"></param>
        /// <param name="baseLayoutFieldId"></param>
        /// <param name="isRoot"></param>
        /// <returns></returns>
        protected IList<Item> GetThemesWithBaseThemes(Item themeItem, ICollection<ID> processedThemes, ID baseLayoutFieldId, bool isRoot = false)
        {
            var source = new List<Item>();

            processedThemes.Add(themeItem.ID);

            var item = isRoot ? GetThemeItem() : themeItem;

            foreach (var themItem in ((MultilistField)item.Fields[baseLayoutFieldId]).GetItems())
            {
                if (processedThemes.Contains(themItem.ID)) continue;

                foreach (var themesWithBaseTheme in this.GetThemesWithBaseThemes(themItem, processedThemes, baseLayoutFieldId))
                {
                    var theme = themesWithBaseTheme;

                    if (source.All(t => t.ID != theme.ID))
                    {
                        source.Add(theme);
                    }
                }
            }

            if (source.All(t => t.ID != themeItem.ID))
            {
                source.Add(themeItem);
            }

            return source;
        }

        /// <summary>
        /// Return either the context item or the page design where the base themes
        /// are selected.
        /// </summary>
        /// <returns></returns>
        protected Item GetThemeItem()
        {
            try
            {
                var item = Sitecore.Context.Item;

                if (item.Fields[Templates.Theme.Fields.OverrideBaseLayout].Value == "1") return item;

                var pageDesign = item.Fields[Templates.Design.Fields.PageDesignField].Value;

                if (string.IsNullOrEmpty(pageDesign))
                {
                    var rootItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.RootPath);

                    var design = rootItem?.Children?
                        .FirstOrDefault(x => x.TemplateID.Equals(Templates.Design.Presentation))?.Children
                        .FirstOrDefault(y => y.TemplateID.Equals(Templates.Design.PageDesign));

                    if (design == null) return item;

                    var rawValue = design.Fields[Templates.Design.Fields.TemplatesMapping].Value;

                    var str = HttpUtility.UrlDecode(HttpUtility.UrlDecode(rawValue));

                    if (string.IsNullOrEmpty(str)) return item;

                    var templateMappings = str.Split('&');

                    foreach (var templateMapping in templateMappings)
                    {
                        var entry = templateMapping.Split('=');

                        var template = entry[0];

                        var tempPageDesign = entry[1];

                        if (!item.TemplateID.Equals(new ID(template))) continue;

                        pageDesign = tempPageDesign;

                        break;
                    }
                }

                item = Sitecore.Context.Database.GetItem(pageDesign);

                return item;
            }
            catch (Exception e)
            {
                Log.Error($"An error occurred when retrieving Base Themes. The ones selected at the page {Sitecore.Context.Item.ID.Guid} will be used: ",
                    e, this);

                return Sitecore.Context.Item;
            }
        }
    }
}