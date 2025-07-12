import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { getCouponByCode, updateCoupon } from "../../services/couponService";
import { useAuth0 } from "@auth0/auth0-react";

const UpdateCoupon = () => {
  const { couponCode } = useParams();
  const navigate = useNavigate();

  const [couponId, setCouponId] = useState<number | null>(null);
  const [code, setCode] = useState("");
  const [discountAmount, setDiscountAmount] = useState<number>(0);
  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    const fetchCoupon = async () => {
      if (!couponCode) return;

      try {
        const coupon = await getCouponByCode(couponCode);
        setCouponId(coupon.couponId);
        setCode(coupon.couponCode);
        setDiscountAmount(coupon.discountAmount);
      } catch (error) {
        toast.error("Error when loading the coupon");
      }
    };

    fetchCoupon();
  }, [couponCode]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    if (couponId === null) return;

    try {
      const token = await getAccessTokenSilently();
      await updateCoupon(couponId, {
        couponCode: code,
        discountAmount,
      }, token);

      toast.success("The coupon has been successfully updated!");
      navigate("/coupons");
    } catch (error) {
      toast.error("Error when updating the coupon");
    }
  };

  return (
    <div className="p-4 max-w-md mx-auto">
      <h2 className="text-2xl font-bold mb-4">Edit Coupon</h2>
      <form onSubmit={handleSubmit} className="flex flex-col gap-4">
        <input
          type="text"
          className="border p-2 rounded"
          placeholder="Coupon code"
          value={code}
          onChange={(e) => setCode(e.target.value)}
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
        <button
          type="submit"
          className="bg-blue-500 text-white p-2 rounded hover:bg-blue-600"
        >
          Save
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

export default UpdateCoupon;
