import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { ProductCreateUpdate } from "../../types/ProductCreateUpdate";
import { deleteProduct, getProductById } from "../../services/productService";
import { useAuth0 } from "@auth0/auth0-react";

const DeleteProduct = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [product, setProduct] = useState<ProductCreateUpdate | null>(null);
  const [loading, setLoading] = useState(true);
  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    const fetchProduct = async () => {
      try {
        const fetched = await getProductById(Number(id));
        setProduct(fetched);
      } catch (err) {
        toast.error("Failed to load product");
      } finally {
        setLoading(false);
      }
    };

    fetchProduct();
  }, [id]);

  const handleDelete = async () => {
    try {
      const token = await getAccessTokenSilently();
      await deleteProduct(Number(id), token);
      toast.success("Product deleted successfully");
      navigate("/products");
    } catch (err) {
      toast.error("Error deleting product");
    }
  };

  if (loading) return <div className="p-4">Loading...</div>;
  if (!product) return <div className="p-4">Product not found</div>;

  return (
    <div className="p-4 max-w-xl mx-auto">
      <h1 className="text-2xl font-bold mb-4">Delete Product</h1>
      <p>Are you sure you want to delete the following product?</p>
      <div className="border p-4 my-4 rounded shadow">
        <p><strong>Name:</strong> {product.name}</p>
        <p><strong>Price:</strong> ${product.price}</p>
        <p><strong>Category:</strong> {product.categoryName}</p>
        <p><strong>Description:</strong> {product.description || "None"}</p>
      </div>
      <button
        onClick={handleDelete}
        className="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700"
      >
        Delete
      </button>
      <button
        onClick={() => navigate("/products")}
        className="ml-2 px-4 py-2 border rounded"
      >
        Cancel
      </button>
    </div>
  );
};

export default DeleteProduct;
