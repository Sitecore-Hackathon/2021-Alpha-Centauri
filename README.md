# Hackathon Submission Entry form

## Team name
Alpha Centauri

## Category
Best enhancement for SXA.

## Description
For this year's Sitecore Hackathon we chose to do the best enhancement for SXA category. With our module it will eliminate a lot of unnecessary script/style themes that are on pages in an SXA website. When this module is installed in an SXA website the site will not only be easier to manage, but it will be faster since pages will only use the script/style themes that they need. 

The module overwrites the existing styles/scripts themes inject process of SXA using a theme selector. Inserting the needed scripts and styles for pages leaving out the unneeded ones for pages. Users have the option to add and remove any script/style themes for any page type and also specific pages. If nothing is chosen then the page will be defaulted to the standard SXA styles/scripts.

## Video link
You can find our video at:
https://youtu.be/aKOQfb838dM

## Pre-requisites and Dependencies
- SXA latest version for Sitecore 10.1 installed.

## Installation instructions
Please note this assumes you have an SXA store tenant installed which is required for an SXA site.

1. Install the Sitecore package install-packages\AlphaCentauri-SitecoreHackathon-2021.zip using the installation package installer in Sitecore. Overwrite the existing items.
2. Publish the site in your Sitecore instance.
3. Verify in the bin folder of the site you have the following dlls. AlphaCentauri.XA.Foundation.Theming.dll
4. We have included a page to use as a test. Go to [yoursiteurl]/page.
5. Please follow the video link above for how to use instructions if needed. 

### Configuration
No configuration changes needed.

## Usage instructions
Any page that will inherit from the new template will have a selection list. Once script/style themes are chosen those will be the ones that are rendered on the page as long as the checkbox to overide the base themes is checked. SXA defaults will apply if not. See image below:
![Selectors Checkbox Screenshot](docs/images/screenshotofselectorcheckbox.png?raw=true "Selectors Checkbox Screenshot")

Pages will inherit from the Page Design template that was created for this change.
![Page Design Template](docs/images/pagedesigntemplate.png?raw=true "Page Design Template")

Pages will inherit from the Page Design template that was created for this change.
![Default Page Theme](docs/images/defaultpagethemes.png?raw=true "Default Page Theme")

## Comments
Please follow the video that covers all the different scenarios. Thank you for a great Hackathon.