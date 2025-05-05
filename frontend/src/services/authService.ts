import { UserManager, WebStorageStateStore } from "oidc-client-ts";
import {oidcConfig} from "../auth-config";

const userManager = new UserManager({
  ...oidcConfig,
  userStore: new WebStorageStateStore({ store: window.localStorage }),
});

export const signinRedirect = () => userManager.signinRedirect();

export const signinRedirectCallback = () => userManager.signinRedirectCallback();

export const signoutRedirect = () => userManager.signoutRedirect();

export const getUser = () => userManager.getUser();

export const removeUser = () => userManager.removeUser();

export default userManager;
