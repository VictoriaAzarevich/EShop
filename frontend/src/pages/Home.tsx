import { useEffect, useState } from "react";
import { getAllProducts } from "../services/productService";
import { ProductResponse } from "../types/ProductResponse";

import '../App.css';
const Home = () => {
  const [products, setProducts] = useState<ProductResponse[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [loading, setLoading] = useState(true);

  const fetchProducts = async (page: number) => {
    setLoading(true);
    try {
      const data = await getAllProducts(page, 8); // например, по 8 товаров на страницу
      setProducts(data.products);
      setTotalPages(data.totalPages);
    } catch (error) {
      console.error("Failed to load products", error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchProducts(currentPage);
  }, [currentPage]);

  const handlePageChange = (newPage: number) => {
    if (newPage >= 1 && newPage <= totalPages) {
      setCurrentPage(newPage);
    }
  };

  if (loading) return <p className="text-center text-lg mt-10">Loading...</p>;

  return (
    <div className="max-w-6xl mx-auto px-4 py-8">
      <h1 className="text-3xl font-bold mb-8 text-center text-gray-800">Products</h1>
      <div className="grid gap-8 grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4">
        {products.map((product) => (
          <div
            key={product.productId}
            className="bg-white rounded-2xl shadow-lg hover:shadow-xl transition-shadow duration-300 p-4 flex flex-col"
          >
            <img
              src={product.imageUrl || "https://via.placeholder.com/300x200"}
              alt={product.name}
              className="rounded-lg h-48 object-cover mb-4"
            />
            <h2 className="text-lg font-semibold text-gray-800">{product.name}</h2>
            <p className="text-sm text-gray-500 mt-1 flex-grow">{product.description}</p>
            <p className="text-blue-600 font-bold mt-2 text-lg">${product.price}</p>
          </div>
        ))}
      </div>

      {/* Pagination */}
      <div className="flex justify-center mt-10 gap-4">
        <button
          onClick={() => handlePageChange(currentPage - 1)}
          disabled={currentPage === 1}
          className="px-4 py-2 bg-gray-200 hover:bg-gray-300 rounded disabled:opacity-50"
        >
          Previous
        </button>
        <span className="px-4 py-2 text-gray-700 font-medium">
          Page {currentPage} of {totalPages}
        </span>
        <button
          onClick={() => handlePageChange(currentPage + 1)}
          disabled={currentPage === totalPages}
          className="px-4 py-2 bg-gray-200 hover:bg-gray-300 rounded disabled:opacity-50"
        >
          Next
        </button>
      </div>
    </div>
  );
};

export default Home;
