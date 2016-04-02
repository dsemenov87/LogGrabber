# LogGrabber

### Summary ###

The implementation of the system for collecting logs
similar to https://raygun.io/. It receives information over HTTP.
And able to display these logs on the website.
Access to the logs is carried by login / password.

### Requirements ###

Uses ASP.NET5 (DNX 4.5.1 and DNX Core 5.0 schould be installed):

* https://docs.asp.net/en/latest/getting-started/index.html
* before starting a web application, it is necessary to create the database. Enter in *PowerShell* as follows:
```ps
cd /YourProjectPath/LogGrabber/src/LogGrabber.Web
dnu restore
dnx ef database update
``` 

