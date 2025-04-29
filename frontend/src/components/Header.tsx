import { Link } from "react-router-dom";

const Header = () => {
  return (
    <header className="bg-gray-800 text-white p-4 shadow">
      <nav className="container mx-auto flex justify-between items-center">
        <h1 className="text-xl font-bold">EShop</h1>
        <div className="space-x-4">
          <Link to="/" className="hover:underline">
            Home
          </Link>
          <Link to="/categories" className="hover:underline">
            Category management
          </Link>
          <Link to="/products" className="hover:underline">
            Product management
          </Link>
        </div>
      </nav>
    </header>
  );
};

export default Header;
