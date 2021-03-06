# Hackathon Submission Entry form

## Team name
Alpha Centauri

## Category
Best enhancement for SXA.

## Description
For this year's Sitecore Hackathon we chose to do the best enhancement for SXA category. With our module it will eliminate a lot of unnecessary script/style themes that are on pages in an SXA website. When this module is installed in an SXA website the site will not only be easier to manage, but it will be faster since pages will only use the script/style themes that they need. 

The module overwrites the existing styles/scripts themes inject process of SXA using a theme selector. Inserting the needed scripts and styles for pages leaving out the unneeded ones for pages. Users have the option to add and remove any script/style themes for any page type and also specific pages. If nothing is chosen then the page will be defaulted to the standard SXA styles/scripts.

## Video link
⟹ Provide a video highlighing your Hackathon module submission and provide a link to the video. You can use any video hosting, file share or even upload the video to this repository. _Just remember to update the link below_

⟹ [Replace this Video link](#video-link)

## Pre-requisites and Dependencies
- SXA latest version for Sitecore 10.1 installed.

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
No configuration changes needed.

## Usage instructions
Any page that will inherit from the new template will have a selection list. Once script/style themes are chosen those will be the ones that are rendered on the page as long as the checkbox to overide the base themes is checked. SXA defaults will apply if not. See image below:
![Selectors Checkbox Screenshot](docs/images/screenshotofselectorcheckbox.png?raw=true "Selectors Checkbox Screenshot")

Pages will inherit from the Page Design template that was created for this change.
![Page Design Template](docs/images/pagedesigntemplate.png?raw=true "Page Design Template")

⟹ Provide documentation about your module, how do the users use your module, where are things located, what do the icons mean, are there any secret shortcuts etc.

Include screenshots where necessary. You can add images to the `./images` folder and then link to them from your documentation:

![Hackathon Logo](docs/images/hackathon.png?raw=true "Hackathon Logo")


## Comments
If you'd like to make additional comments that is important for your module entry.