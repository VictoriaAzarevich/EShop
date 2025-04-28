import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getCategories, deleteCategory } from "../services/categoryService"; 
import { CategoryResponse } from "../types/CategoryResponse"; 

const Categories = () => {
  const [categories, setCategories] = useState<CategoryResponse[]>([]);
  const navigate = useNavigate();

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    const data = await getCategories();
    setCategories(data);
  };

  const handleDelete = async (categoryId: number) => {
    if (confirm("Точно удалить категорию?")) {
      await deleteCategory(categoryId);
      await fetchCategories();
    }
  };

  return (
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Категории</h1>

      <button
        onClick={() => navigate("/create-category")}
        className="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded mb-4"
      >
        Create category
      </button>

      <div className="space-y-4">
        {categories.map((category) => (
          <div key={category.categoryId} className="flex items-center justify-between border p-4 rounded shadow">
            <span>{category.categoryName}</span>
            <div className="space-x-2">
              <button
                onClick={() => navigate(`/categories/edit/${category.categoryId}`)}
                className="bg-yellow-400 hover:bg-yellow-500 text-white font-bold py-2 px-4 rounded"
              >
                Edit
              </button>
              <button
                onClick={() => handleDelete(category.categoryId)}
                className="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-4 rounded"
              >
                Delete
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Categories;
