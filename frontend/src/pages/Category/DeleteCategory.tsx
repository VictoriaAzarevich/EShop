import { useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { deleteCategory } from "../../services/categoryService";

const DeleteCategory = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    const confirmAndDelete = async () => {
      if (!id) {
        toast.error("The category ID was not found");
        navigate("/categories");
        return;
      }

      const confirm = window.confirm("Are you sure you want to delete this category?");
      if (!confirm) {
        navigate("/categories");
        return;
      }

      try {
        await deleteCategory(Number(id));
        toast.success("The category was successfully deleted");
        navigate("/categories");
      } catch (error) {
        toast.error("Error when deleting a category");
        console.error(error);
      }
    };

    confirmAndDelete();
  }, [id, navigate]);

  return null; 
};

export default DeleteCategory;
