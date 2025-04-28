import { useState } from "react";
import { createCategory } from "../services/categoryService";
import { useNavigate } from "react-router-dom"; 
import { toast } from "react-toastify"; 

const CreateCategory = () => {
  const [categoryName, setCategoryName] = useState("");
  const navigate = useNavigate(); 

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      await createCategory({ categoryName: categoryName }); 
      
      toast.success("Категория успешно создана!"); 
      
      setTimeout(() => {
        navigate("/categories"); 
      }, 1500); 
    } catch (error) {
      console.error("Ошибка при создании категории", error);
      toast.error("Ошибка при создании категории");
    }
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <h2 className="text-2xl font-bold mb-4">Создать категорию</h2>
      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <input
          className="border p-2 rounded"
          type="text"
          placeholder="Название категории"
          value={categoryName}
          onChange={(e) => setCategoryName(e.target.value)}
          required
        />
        <button type="submit" className="bg-blue-500 text-white p-2 rounded hover:bg-blue-600">
          Создать
        </button>
      </form>
    </div>
  );
};

export default CreateCategory;
