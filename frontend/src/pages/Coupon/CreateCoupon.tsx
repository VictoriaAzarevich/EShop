import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import { createCoupon } from "../../services/couponService";
import { useAuth0 } from "@auth0/auth0-react";

const CreateCoupon = () => {
  const [couponCode, setCouponCode] = useState("");
  const [discountAmount, setDiscountAmount] = useState<number>(0);
  const navigate = useNavigate();
  const { getAccessTokenSilently } = useAuth0();

  const handleSubmit = async (e: React.FormEvent) => {
  e.preventDefault();

  try {
    const token = await getAccessTokenSilently();
    await createCoupon({ couponCode, discountAmount }, token);

    toast.success("The coupon was successfully created!");

    setTimeout(() => {
      navigate("/coupons");
    }, 1500);
  } catch (error: any) {
    console.error("Error when creating a coupon", error);

    const serverMessage =
      error?.response?.data?.message || error?.message || "Error when creating a coupon";

    toast.error(serverMessage);
  }
};


  return (
    <div className="p-4 max-w-md mx-auto">
      <h2 className="text-2xl font-bold mb-4">Create Coupon</h2>
      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <input
          className="border p-2 rounded"
          type="text"
          placeholder="Coupon code"
          value={couponCode}
          onChange={(e) => setCouponCode(e.target.value)}
          required
        />
        <input
          className="border p-2 rounded"
          type="number"
          placeholder="Discount amount"
          value={discountAmount}
          onChange={(e) => setDiscountAmount(parseFloat(e.target.value))}
          min={0.01}
          step={0.01}
          required
        />
        <button type="submit" className="bg-blue-500 text-white p-2 rounded hover:bg-blue-600">
          Create Coupon
        </button>
      </form>
      <button
        onClick={() => navigate("/coupons")}
        className="ml-2 px-4 py-2 border rounded mt-2"
      >
        Cancel
      </button>
    </div>
  );
};

export default CreateCoupon;
