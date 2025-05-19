import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { getCategories } from "../../services/categoryService";
import { createProduct } from "../../services/productService";
import { useAuth0 } from "@auth0/auth0-react";

const CreateProduct = () => {
  const [name, setName] = useState<string>("");
  const [price, setPrice] = useState<number>(0);
  const [description, setDescription] = useState<string>("");
  const [categoryId, setCategoryId] = useState<number | undefined>(undefined);
  const [categories, setCategories] = useState<any[]>([]);  
  const [image, setImage] = useState<File | undefined>(undefined);
  const { getAccessTokenSilently } = useAuth0();

  const navigate = useNavigate();

  useEffect(() => {
    const fetchCategories = async () => {
      const categoryList = await getCategories();
      setCategories(categoryList);
    };

    fetchCategories();
  }, []);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    if (categoryId === undefined) {
      toast.error("Select a category");
      return;
    }

    const formData = new FormData();
    formData.append("name", name);
    formData.append("price", price.toString());
    if (description) formData.append("description", description);
    formData.append("categoryId", categoryId.toString());
    if (image) formData.append("image", image);

    try {
      const token = await getAccessTokenSilently();
      await createProduct(formData, token);
      toast.success("The product has been added successfully!");
      setTimeout(() => navigate("/"), 1500); 
    } catch (error) {
      toast.error("Error when adding the product.");
    }
  };

  return (
    <div className="container p-4">
      <h1 className="text-2xl font-bold mb-4">Add a product</h1>
      <form onSubmit={handleSubmit} className="space-y-4">
        <div>
          <label htmlFor="name" className="block font-medium">Product name</label>
          <input
            type="text"
            id="name"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
            className="mt-1 p-2 border rounded w-full"
          />
        </div>

        <div>
          <label htmlFor="price" className="block font-medium">Price</label>
          <input
            type="number"
            id="price"
            value={price}
            onChange={(e) => setPrice(Number(e.target.value))}
            required
            min="1"
            className="mt-1 p-2 border rounded w-full"
          />
        </div>

        <div>
          <label htmlFor="description" className="block font-medium">Description</label>
          <textarea
            id="description"
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            className="mt-1 p-2 border rounded w-full"
          />
        </div>

        <div>
          <label htmlFor="categoryId" className="block font-medium">Category</label>
          <select
            id="categoryId"
            value={categoryId ?? ""}
            onChange={(e) => setCategoryId(Number(e.target.value))}
            required
            className="mt-1 p-2 border rounded w-full"
          >
            <option value="">Select a category</option>
            {categories.map((category) => (
              <option key={category.categoryId} value={category.categoryId}>
                {category.categoryName}
              </option>
            ))}
          </select>
        </div>

        <div>
          <label htmlFor="image" className="block font-medium">Image</label>
          <input
            type="file"
            id="image"
            onChange={(e) => setImage(e.target.files ? e.target.files[0] : undefined)}
            className="mt-1 p-2 border rounded w-full"
          />
        </div>

        <div>
          <button
            type="submit"
            className="bg-blue-500 text-white p-2 rounded hover:bg-blue-600"
          >
            Create a product
          </button>

          <button
        onClick={() => navigate("/products")}
        className="ml-2 px-4 py-2 border rounded"
      >
        Cancel
      </button>
        </div>
      </form>
    </div>
  );
};

export default CreateProduct;
