import { useParams, useNavigate } from "react-router-dom";
import axios from "axios";
import { useEffect } from "react";

const DeleteCategory = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    const deleteCategory = async () => {
      try {
        await axios.delete(`https://localhost:7230/api/category/${id}`);
        navigate("/");
      } catch (error) {
        console.error("Ошибка при удалении категории", error);
      }
    };

    deleteCategory();
  }, [id, navigate]);

  return (
    <div className="p-4 text-center">
      <h2 className="text-xl font-bold">Удаление категории...</h2>
    </div>
  );
};

export default DeleteCategory;
