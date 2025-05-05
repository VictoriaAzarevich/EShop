import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { signinRedirectCallback } from "../services/authService";

const CallbackPage = () => {
  const navigate = useNavigate();

  useEffect(() => {
    signinRedirectCallback().then(() => {
      navigate("/");
    });
  }, [navigate]);

  return <p>Completing sign-in...</p>;
};

export default CallbackPage;
