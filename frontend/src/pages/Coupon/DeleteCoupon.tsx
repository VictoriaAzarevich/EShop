import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { toast } from "react-toastify";
import { getCouponByCode, deleteCoupon } from "../../services/couponService";
import { CouponResponse } from "../../types/CouponResponse";
import { useAuth0 } from "@auth0/auth0-react";

const DeleteCoupon = () => {
  const { couponCode } = useParams<{ couponCode: string }>();
  const navigate = useNavigate();

  const [coupon, setCoupon] = useState<CouponResponse | null>(null);
  const [loading, setLoading] = useState(true);
  const { getAccessTokenSilently } = useAuth0();

  useEffect(() => {
    const fetchCoupon = async () => {
      try {
        if (!couponCode) return;
        const data = await getCouponByCode(couponCode);
        setCoupon(data);
      } catch (err) {
        toast.error("Failed to load coupon");
      } finally {
        setLoading(false);
      }
    };

    fetchCoupon();
  }, [couponCode]);

  const handleDelete = async () => {
    if (!coupon) return;

    try {
      const token = await getAccessTokenSilently();
      await deleteCoupon(coupon.couponId, token);
      toast.success("Coupon deleted successfully");
      navigate("/coupons");
    } catch (err) {
      toast.error("Error deleting coupon");
    }
  };

  if (loading) return <div className="p-4">Loading...</div>;
  if (!coupon) return <div className="p-4">Coupon not found</div>;

  return (
    <div className="p-4 max-w-xl mx-auto">
      <h1 className="text-2xl font-bold mb-4">Delete Coupon</h1>
      <p>Are you sure you want to delete the following coupon?</p>
      <div className="border p-4 my-4 rounded shadow">
        <p><strong>Coupon Code:</strong> {coupon.couponCode}</p>
        <p><strong>Discount Amount:</strong> ${coupon.discountAmount}</p>
      </div>
      <button
        onClick={handleDelete}
        className="bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700"
      >
        Delete
      </button>
      <button
        onClick={() => navigate("/coupons")}
        className="ml-2 px-4 py-2 border rounded"
      >
        Cancel
      </button>
    </div>
  );
};

export default DeleteCoupon;
