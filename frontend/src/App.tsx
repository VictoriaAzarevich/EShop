import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Products from "./pages/Product/Products";
import CreateProduct from "./pages/Product/CreateProduct";
import UpdateProduct from "./pages/Product/UpdateProduct";
import DeleteProduct from "./pages/Product/DeleteProduct";
import Categories from "./pages/Category/Categories";
import CreateCategory from "./pages/Category/CreateCategory";
import UpdateCategory from "./pages/Category/UpdateCategory";
import DeleteCategory from "./pages/Category/DeleteCategory";
import CartPage from "./pages/Cart/Cart"
import Header from "./components/Header";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';


function App() {
  return (
    <BrowserRouter>
    <Header />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/products" element={<Products />} />
        <Route path="/create-product" element={<CreateProduct />} />
        <Route path="/update-product/:id" element={<UpdateProduct />} />
        <Route path="/delete-product/:id" element={<DeleteProduct />} />
        <Route path="/categories" element={<Categories />} />
        <Route path="/create-category" element={<CreateCategory />} />
        <Route path="/update-category/:id" element={<UpdateCategory />} />
        <Route path="/delete-category/:id" element={<DeleteCategory />} />
        <Route path="/cart/:userId" element={<CartPage />} /> 
      </Routes>
      <ToastContainer />
    </BrowserRouter>
  );
}

export default App
