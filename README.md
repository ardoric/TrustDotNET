# TrustDotNET

Sample application to showcase how to add trusted root authorities in .NET via code

The pages will make a request to a server with an untrusted root (https://untrusted-root.badssl.com), hash that content and show it on the page.

## Program.cs

Here we set up the ServicePointManager which some TLS APIs use

## WebRequest.cshtml.cs

This uses WebRequest (deprecated) and works out of the box from the ServicePointManager set up done on Program.cs

## HttpClientError.cshtml.cs

This uses HttpClient and will fail because this API does not use ServicePointManager

## HttpClient.cshtml.cs

This uses HttpClient and will succeed since it is configured to use HttpClientHandler to override the default behavior