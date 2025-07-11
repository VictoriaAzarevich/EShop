import { useAuth0 } from "@auth0/auth0-react";
import { Link, useNavigate, useLocation } from "react-router-dom";

const Header = () => {
  const navigate = useNavigate();
  const { loginWithRedirect, logout, isAuthenticated, user } = useAuth0();
  const location = useLocation();
  const isHome = location.pathname === "/";

  const handleCartClick = () => {
    if (isAuthenticated) {
      const userId = user?.sub ?? "unknown-user";
      navigate(`/cart/${userId}`);
    } else {
      loginWithRedirect(); 
    }
  };

  const isAdmin = user?.["https://eshop.api.com/roles"]?.includes("admin");

  return (
    <header className="bg-gray-800 text-white p-4 shadow">
      <nav className="container mx-auto flex justify-between items-center">
        <h1 className="text-xl font-bold">EShop</h1>
        <div className="space-x-4">
          {!isHome && (
          <Link to="/" className="hover:underline">
            Home
          </Link>
        )}

          {isAuthenticated && isAdmin && (
            <>
              <Link to="/categories" className="hover:underline">Category management</Link>
              <Link to="/products" className="hover:underline">Product management</Link>
              <Link to="/coupons" className="hover:underline">Coupon management</Link>
            </>
          )}

          {isAuthenticated ? (
            <>
              <button onClick={handleCartClick} className="hover:underline">Cart</button>
              <button
                onClick={() =>
                  logout({
                    logoutParams: {
                      returnTo: window.location.origin,
                    },
                  }) 
                } className="hover:underline"
              >
                Log out
              </button>
            </>
          ) : (
            <button onClick={() => loginWithRedirect()} className="hover:underline">
              Sign In
            </button>
          )}
        </div>
      </nav>
    </header>
  );
};

export default Header;
