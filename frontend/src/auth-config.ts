export const oidcConfig = {
  authority: "https://localhost:5001", 
  client_id: "react-client",
  redirect_uri: "http://localhost:3000/callback",
  post_logout_redirect_uri: "http://localhost:3000/",
  response_type: "code",
  scope: "openid profile roles api1", 
};
