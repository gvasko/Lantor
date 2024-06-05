import { BrowserCacheLocation, Configuration, LogLevel } from "@azure/msal-browser";

export const msalConfig: Configuration = {
  auth: {
    clientId: 'd478905a-7e64-4ad9-8636-f74ad6942c93',
    authority: 'https://login.microsoftonline.com/c3177814-4713-490a-8352-ef79204f200e',
    redirectUri: '/auth',
    postLogoutRedirectUri: '/'
  },
  cache: {
    cacheLocation: BrowserCacheLocation.LocalStorage,
    storeAuthStateInCookie: false,
  },
  system: {
    loggerOptions: {
      loggerCallback(logLevel: LogLevel, message: string) {
        console.log(message);
      },
      logLevel: LogLevel.Verbose,
      piiLoggingEnabled: false,
    },
  },
};

