import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { getCategories } from "../services/categoryService";
import { getProductById, updateProduct } from "../services/productService";
import { CategoryResponse } from "../types/CategoryResponse";
import { toast } from "react-toastify";
import { ProductCreateUpdate } from "../types/ProductCreateUpdate";

const UpdateProduct = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [product, setProduct] = useState<ProductCreateUpdate | null>(null);
  const [categories, setCategories] = useState<CategoryResponse[]>([]);
  const [name, setName] = useState("");
  const [price, setPrice] = useState<number>(0);
  const [description, setDescription] = useState("");
  const [categoryId, setCategoryId] = useState<number | undefined>(undefined);
  const [image, setImage] = useState<File | undefined>(undefined);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const product = await getProductById(Number(id));
        setProduct(product);
        setName(product.name);
        setPrice(product.price);
        setDescription(product.description || "");
        setCategoryId(product.categoryId);

        const categories = await getCategories();
        setCategories(categories);
      } catch (err) {
        toast.error("Failed to load product");
      }
    };
    fetchData();
  }, [id]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
  
    const formData = new FormData();
    formData.append("name", name);
    formData.append("price", price.toString());
    formData.append("description", description);
    formData.append("categoryId", categoryId!.toString());

    const selectedCategory = categories.find(cat => cat.categoryId === categoryId);
    if (selectedCategory) {
        formData.append("categoryName", selectedCategory.categoryName);
    }
  
    if (image) {
      formData.append("image", image);
      formData.append("imageUrl", "null"); 
    } else {
      if (product?.imageUrl) {
        formData.append("imageUrl", product.imageUrl);
      }
    }
  
    try {
      await updateProduct(Number(id), formData);
      toast.success("Product updated successfully!");
      setTimeout(() => navigate("/products"), 1500);
    } catch (err) {
      toast.error("Error updating product");
    }
  };
  

  if (!product) return <div className="p-4">Loading...</div>;

  return (
    <div className="p-4 max-w-xl mx-auto">
      <h1 className="text-2xl font-bold mb-4">Update Product</h1>
      <form onSubmit={handleSubmit} className="space-y-4">
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          className="w-full p-2 border rounded"
          placeholder="Product name"
          required
        />
        <input
          type="number"
          value={price}
          onChange={(e) => setPrice(Number(e.target.value))}
          className="w-full p-2 border rounded"
          placeholder="Price"
          required
        />
        <textarea
          value={description}
          onChange={(e) => setDescription(e.target.value)}
          className="w-full p-2 border rounded"
          placeholder="Description"
        />
        <select
          value={categoryId}
          onChange={(e) => setCategoryId(Number(e.target.value))}
          className="w-full p-2 border rounded"
          required
        >
          <option value="">Select category</option>
          {categories.map((cat) => (
            <option key={cat.categoryId} value={cat.categoryId}>
              {cat.categoryName}
            </option>
          ))}
        </select>
        <input
          type="file"
          onChange={(e) => setImage(e.target.files?.[0])}
          className="w-full p-2 border rounded"
        />
        <button type="submit" className="bg-blue-500 text-white p-2 rounded hover:bg-blue-600">
          Save
        </button>
      </form>
    </div>
  );
};

export default UpdateProduct;
