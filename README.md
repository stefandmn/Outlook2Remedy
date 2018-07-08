# Outlook2Remedy

**Outlook2Remedy** is an Microsoft Outlook add-in that allows you to handle emails 
through a Windows mail profile (usually described by a MAPI configuration) and to 
create tickets into **BMC Remedy** ticketing system using a staging area, allowing 
you then to transform staging records into standard or custom tickets by adding simple 
workflow to push data into the corresponding ticketing area (Incident Management, 
Service Request Management or a custom ticketing application developed over BMC Remedy
environment).


## Solution
The architecture has two layers: 
 - Outlook addin module that will bring a dedicated area within Outlook to convert
   email into tickets and also to configure the entire process.
 - Remedy interface workflow that is a definition file stored in `def` folder (see 
   in the distribution archive or in project structure) 

After installation of add-in binaries Outlook will show a new tab called _REMEDY_ 
having on two specific buttons:

![Outlook2Remedy Outlook Add-in - Actions Area](doc/outlook.png)
 
 * **Convert2Ticket** | Selecting any email document from _Inbox_ or from any other 
 folder from Outlook profile will send it in Remedy - more specific into the staging 
 form (`Outlook2Remedy` form), according to the rules and configuration settled for
 this data transfer in Remedy.
 
![Outlook2Remedy Outlook Add-in - Configuration Area](doc/configuration.png)
 
 * **Conversion Settings** | Using this option your can personalize your data transfer 
 flow within both Remedy and Outlook areas; you can specify what should happen with 
 the selected email document after conversion and also you can describe what email
 components are considered in the conversion process.


Remedy workflow (located in `def` folder) must be imported using _BMC Remedy Developer 
Studio_; it is recommended to import it before installation of add-in binaries and two 
aaditional objects will be created within Remedy server container:
 1. `Outlook2Remedy` regular form, considered staging layer in this data conversion flow
 2. `Outlook2Remedy` SOAP web services which makes possible data transfer from Outlook to Remedy.

In addition, it is recommended to run data transfer using a dedicated Remedy account that 
should have write permissions in `Outlook2Remedy` form to access `Outlook2Remedy` 
web services.


## Installation
In order to install **Outlook2Remedy** solution you have to run the following steps:
 1. Download the latest release archive (`Outlook2Remedy.zip` file published in _release_ area)
    and upzip it.
 2. Import `Outlook2Remedy.def` definition file located in `Outlook2Remedy/def` folder using 
    _BMC Remedy Developer Studio_; import the definitions on all servers that are part of your
	environment (test, dev, production, etc.)
 3. Copy the entire `Outlook2Remedy` folder	(made by unzip) into location
    `C:\Users\<username>\AppData\Roaming\Microsoft\AddIns`. The indicated location is per 
	specififc Windows account, so in case you want to install the add-in for many users on 
	the same workstation you have to copy the `Outlook2Remedy` folder in all your user profiles.
 4. Run through double-click `Outlook2Remedy.vsto` installation script located in 
    `C:\Users\<username>\AppData\Roaming\Microsoft\AddIns\Outlook2Remedy` folder. The add-in is 
	signed by a self-sign certificate, so you have to trust it and confirm it during installation.
	In case you need to install it isolated, try to download  `Outlook2Remedy.pfx` file from 
	source code and then to install it in the standard way (the password for private key is 
	_Outlook2Remedy_).
	Note: Before to run `Outlook2Remedy.vsto` installation script close all open 
	_Microsoft Office_ applications (especially _Outlook_)


## Configuration

http://devremedy.company.com:8080/arsys/services/ARService?server=devremedy&webService=Outlook2Remedy

http://devremedy.company.com:8080/arsys/forms/devremedy/SRM%3ARequest/Administrator

### Compatibility