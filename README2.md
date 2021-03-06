
# Hackathon Submission Entry form

> __Important__  
> 
> Copy and paste the content of this file into README.md or face automatic __disqualification__  
> All headlines and subheadlines shall be retained if not noted otherwise.  
> Fill in text in each section as instructed and then delete the existing text, including this blockquote.

You can find a very good reference to Github flavoured markdown reference in [this cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet). If you want something a bit more WYSIWYG for editing then could use [StackEdit](https://stackedit.io/app) which provides a more user friendly interface for generating the Markdown code. Those of you who are [VS Code fans](https://code.visualstudio.com/docs/languages/markdown#_markdown-preview) can edit/preview directly in that interface too.

## Team name
Alpha Centauri
## Category
Best enhancement for SXA.
## Description
For this year's Sitecore Hackathon we chose to do the best enhancement for SXA category. With our module it will eliminate a lot of unnecessary scripts and styles that are on pages in an SXA website. When this module is installed in an SXA website the site will not only be easier to manage, but it will be faster since pages will only use the scripts that they need. 

The module overwrites the existing styles/scripts inject process of SXA using a selector and inserting the needed scripts and styles for pages and not putting ones that are not used on that page. Users have the option to add and remove any script and style for any page type and also specific pages. If nothing is chosen then the page will be defaulted to the standard SXA styles/scripts.

## Video link
⟹ Provide a video highlighing your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_

⟹ [Replace this Video link](#video-link)

## Pre-requisites and Dependencies
- SXA latest version for 10.1 installed.

## Installation instructions
1. Install the Sitecore package _______.zip using the installation package in Sitecore. 
2. Publish the site in Sitecore.
3. Verify in the bin folder you have the following dlls. ______
4. We have included a page to use as a test. Go to [yoursiteurl]/____page goes here.
⟹ Write a short clear step-wise instruction on how to install your module.  

> _A simple well-described installation process is required to win the Hackathon._  
> Feel free to use any of the following tools/formats as part of the installation:
> - Sitecore Package files
> - Docker image builds
> - Sitecore CLI
> - msbuild
> - npm / yarn
> 
> _Do not use_
> - TDS
> - Unicorn

### Configuration
⟹ If there are any custom configuration that has to be set manually then remember to add all details here.

_Remove this subsection if your entry does not require any configuration that is not fully covered in the installation instructions already_

## Usage instructions

⟹ Provide documentation about your module, how do the users use your module, where are things located, what do the icons mean, are there any secret shortcuts etc.

Include screenshots where necessary. You can add images to the `./images` folder and then link to them from your documentation:

![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")

You can embed images of different formats too:

![Selectors Screenshot](docs/images/screenshotofselectors.png?raw=true "Selectors Screenshot")

And you can embed external images too:

![Random](https://thiscatdoesnotexist.com/)

## Comments
If you'd like to make additional comments that is important for your module entry.