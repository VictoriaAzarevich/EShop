import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import CreateProduct from "./pages/CreateProduct";
import Categories from "./pages/Categories";
import CreateCategory from "./pages/CreateCategory";
import UpdateCategory from "./pages/UpdateCategory";
import DeleteCategory from "./pages/DeleteCategory";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/create-product" element={<CreateProduct />} />
        <Route path="/categories" element={<Categories />} />
        <Route path="/create-category" element={<CreateCategory />} />
        <Route path="/update-category/:id" element={<UpdateCategory />} />
        <Route path="/delete-category/:id" element={<DeleteCategory />} />
      </Routes>
      <ToastContainer />
    </BrowserRouter>
  );
}

export default App
