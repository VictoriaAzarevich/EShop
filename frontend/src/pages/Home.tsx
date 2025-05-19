import { useEffect, useState } from "react";
import { getAllProducts } from "../services/productService";
import { getCategories } from "../services/categoryService";
import { ProductResponse } from "../types/ProductResponse";
import { CategoryResponse } from "../types/CategoryResponse";
import { createOrUpdateCart } from "../services/cartService"; 
import { toast } from "react-toastify"; 
import '../App.css';
import { useAuth0 } from "@auth0/auth0-react";

const Home = () => {
  const [products, setProducts] = useState<ProductResponse[]>([]);
  const [categories, setCategories] = useState<CategoryResponse[]>([]);
  const [selectedCategory, setSelectedCategory] = useState<number | undefined>(undefined);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const { getAccessTokenSilently, isAuthenticated, loginWithRedirect, user } = useAuth0();
  const userId = user?.sub ?? "";


  useEffect(() => {
    const fetchData = async () => {
      try {
        const categoryList = await getCategories(); 
        setCategories(categoryList);
      } catch (error) {
        console.error("Ошибка при загрузке категорий:", error);
      }
    };

    fetchData();
  }, []);

  useEffect(() => {
    const fetchProducts = async () => {
      const { products, totalPages } = await getAllProducts(currentPage, 10, selectedCategory);
      setProducts(products);
      setTotalPages(totalPages);
    };
    fetchProducts();
  }, [currentPage, selectedCategory]);

  const handleAddToCart = async (product: ProductResponse) => {
  if (!isAuthenticated) {
    loginWithRedirect();
    return;
  }

  try {
    const token = await getAccessTokenSilently();
    await createOrUpdateCart({
      cartHeader: {
        userId,
      },
      cartDetails: [
        {
          cartDetailsId: 0,
          cartHeaderId: 0,
          productId: product.productId,
          count: 1,
          product,
        },
      ],
    }, token);
    toast.success("Product added to cart");
  } catch (error) {
    toast.error("Failed to add to cart");
  }
};


  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Products</h1>
      <div className="mb-4">
        <select
          className="border rounded p-2"
          value={selectedCategory ?? ""}
          onChange={(e) => {
            const value = e.target.value;
            setSelectedCategory(value ? Number(value) : undefined);
            setCurrentPage(1);
          }}
        >
          <option value="">All Categories</option>
          {categories.map((cat) => (
            <option key={cat.categoryId} value={cat.categoryId}>
              {cat.categoryName}
            </option>
          ))}
        </select>
      </div>

      <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6">
        {products.map((product) => (
          <div key={product.productId} className="border rounded-xl shadow p-4">
            <img src={product.imageUrl} alt={product.name} className="w-full h-40 object-cover rounded mb-2" />
            <h2 className="text-lg font-semibold">{product.name}</h2>
            <p className="text-sm text-gray-600">{product.description}</p>
            <p className="text-blue-600 font-bold mt-2">${product.price}</p>
            <button
              onClick={() => handleAddToCart(product)}
              className="bg-green-500 text-white px-4 py-2 mt-3 rounded hover:bg-green-600"
            >
              Add to Cart
            </button>
          </div>
        ))}
      </div>

      <div className="flex justify-between mt-6">
        <button
          onClick={() => setCurrentPage((prev) => Math.max(1, prev - 1))}
          disabled={currentPage === 1}
          className="bg-gray-200 px-4 py-2 rounded"
        >
          Previous
        </button>
        <span>Page {currentPage} of {totalPages}</span>
        <button
          onClick={() => setCurrentPage((prev) => Math.min(totalPages, prev + 1))}
          disabled={currentPage === totalPages}
          className="bg-gray-200 px-4 py-2 rounded"
        >
          Next
        </button>
      </div>
    </div>
  );
};

export default Home;
