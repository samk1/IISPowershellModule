# IISPowershellModule
IIS Handler for *.ps1 files

This is a really basic IIS handler for *.ps1 files.

All of the output of the powershell script becomes the response body. 
The http context is available in any script executed by this handler as $HTTP_CONTEXT,
which means you can add headers, cookies, or write directly into the response object.
