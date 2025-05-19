import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { getCategoryById, updateCategory } from "../../services/categoryService";
import { useAuth0 } from "@auth0/auth0-react";

const UpdateCategory = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [categoryName, setCategoryName] = useState("");
  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    const fetchCategory = async () => {
      if (!id) return;
      try {
        const category = await getCategoryById(Number(id));
        setCategoryName(category.categoryName);  
      } catch (error) {
        toast.error("Error when loading a category");
      }
    };
    fetchCategory();
  }, [id]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!id) return;

    try {
      const token = await getAccessTokenSilently();
      await updateCategory(Number(id), { categoryName }, token);
      toast.success("The category has been successfully updated!");
      navigate("/categories");
    } catch (error) {
      toast.error("Error when updating a category");
    }
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <h2 className="text-2xl font-bold mb-4">Edit a category</h2>
      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <input
          type="text"
          className="border p-2 rounded"
          value={categoryName}
          onChange={(e) => setCategoryName(e.target.value)}
          required
        />
        <button
          type="submit"
          className="bg-blue-500 text-white p-2 rounded hover:bg-blue-600"
        >
          Save
        </button>
      </form>
      <button
        onClick={() => navigate("/categories")}
        className="ml-2 px-4 py-2 border rounded"
      >
        Cancel
      </button>
    </div>
  );
};

export default UpdateCategory;
