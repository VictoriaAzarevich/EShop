import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import {
  getCoupons,
} from "../../services/couponService";
import { CouponResponse } from "../../types/CouponResponse";

const Coupons = () => {
  const [coupons, setCoupons] = useState<CouponResponse[]>([]);
  const navigate = useNavigate();

  const fetchCoupons = async () => {
    try {
      const data = await getCoupons();
      setCoupons(data);
    } catch {
      toast.error("Failed to load coupons");
    }
  };

  useEffect(() => {
    fetchCoupons();
  }, []);


  return (
    <div className="p-6 max-w-3xl mx-auto">
      <h1 className="text-2xl font-bold mb-4">Coupons</h1>

      <button
        onClick={() => navigate("/create-coupon")}
        className="bg-green-500 hover:bg-green-600 text-white font-bold py-2 px-4 rounded mb-4"
      >
        Create a coupon
      </button>

      <div className="space-y-4">
        {coupons.map((coupon) => (
          <div key={coupon.couponId} className="flex items-center justify-between border p-4 rounded shadow">
            <span>{coupon.couponCode}</span>
            <div className="space-x-2">
              <button
                onClick={() => navigate(`/update-coupon/${coupon.couponCode}`)}
                className="bg-yellow-400 hover:bg-yellow-500 text-white font-bold py-2 px-4 rounded"
              >
                Edit
              </button>
              <button
                onClick={() => navigate(`/delete-coupon/${coupon.couponCode}`)}
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

export default Coupons;
