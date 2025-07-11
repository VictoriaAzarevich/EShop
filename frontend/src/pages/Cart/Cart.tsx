import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { Cart } from "../../types/Cart";
import { applyCoupon, getCartByUserId, removeCartItem, removeCoupon } from "../../services/cartService";
import { useNavigate, useParams } from "react-router-dom";
// import { getCouponByCode } from "../../services/couponService";
import { useAuth0 } from "@auth0/auth0-react";

const CartPage = () => {
  const { userId } = useParams<{ userId: string }>();
  const [cart, setCart] = useState<Cart | null>(null);
  const [isLoading, setIsLoading] = useState(true);
  const [coupon, setCoupon] = useState("");
  const { getAccessTokenSilently } = useAuth0();
  const navigate = useNavigate();

  const fetchCart = async () => {
    if (!userId) return;

    try {
      const token = await getAccessTokenSilently();
      const data = await getCartByUserId(userId, token);
      if (!data || !data.cartHeader) {
        setCart(null); 
      } else {
        setCart(data);
      }
      // if (data.cartHeader.couponCode) {
      //   try {
      //     const couponData = await getCouponByCode(data.cartHeader.couponCode);
      //     setDiscountAmount(couponData.discountAmount || 0);
      //     toast.success("Coupon applied");
      //   } catch (error: any) {
      //     if (error.response && error.response.status === 404) {
      //       toast.error("Coupon not found");
      //     } else {
      //       toast.error("Failed to load coupon");
      //     }
      //     setDiscountAmount(0);
      //   }
      // } else {
      //   setDiscountAmount(0);
      // }
    } catch (err) {
      toast.error("Failed to fetch cart");
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    fetchCart();
  }, [userId]);

  const handleRemoveItem = async (cartDetailsId: number) => {
    try {
      const token = await getAccessTokenSilently();
      await removeCartItem(cartDetailsId, token);
      toast.success("Item removed");
      fetchCart();
    } catch (err) {
      toast.error("Failed to remove item");
    }
  };

  const handleApplyCoupon = async () => {
    try {
      const token = await getAccessTokenSilently();
      if (!cart) return;
      const updated = {
        ...cart,
        cartHeader: { ...cart.cartHeader, couponCode: coupon },
      };
      await applyCoupon(updated, token);
      fetchCart();
    } catch (err) {
      toast.error("Failed to apply coupon");
    }
  };

  const handleRemoveCoupon = async () => {
    if (!userId) return;
    try {
      const token = await getAccessTokenSilently();
      await removeCoupon(userId, token);
      toast.success("Coupon removed");
      fetchCart();
    } catch (err) {
      toast.error("Failed to remove coupon");
    }
  };

  if (isLoading) return <div className="p-4">Loading...</div>;

if (!cart || !cart.cartDetails || cart.cartDetails.length === 0) {
  return <div className="p-4 text-lg">Your cart is empty.</div>;
}

const totalAmount = cart.cartDetails.reduce(
  (sum, item) => sum + item.count * item.product.price,
  0
);

  return (
    <div className="p-4 max-w-2xl mx-auto">
      <h1 className="text-2xl font-bold mb-4">Your Cart</h1>

      {cart.cartDetails.length === 0 ? (
        <p>Your cart is empty.</p>
      ) : (
        <>
          <ul className="space-y-4">
            {cart.cartDetails.map((item) => {
              const itemTotal = item.count * item.product.price;
              return (
                <li
                  key={item.cartDetailsId}
                  className="border p-4 rounded shadow-sm"
                >
                  {item.product.imageUrl && (
        <img
          src={item.product.imageUrl}
          alt={item.product.name}
          className="w-24 h-24 object-cover rounded"
        />
      )}
                  <div className="flex justify-between">
                    <div>
                      <p className="font-semibold">{item.product.name}</p>
                      <p className="text-gray-600 mb-1">{item.product.description}</p>
                      <p>Quantity: {item.count}</p>
                      <p>Price: ${item.product.price.toFixed(2)}</p>
                      <p className="font-semibold">
                        Total: ${itemTotal.toFixed(2)}
                      </p>
                    </div>
                    <button
                      className="text-red-500 hover:underline"
                      onClick={() => handleRemoveItem(item.cartDetailsId)}
                    >
                      Remove
                    </button>
                  </div>
                </li>
              );
            })}
          </ul>

          <div className="mt-6 text-right space-y-1">
            <p className="text-lg font-bold">
              Total Amount: ${totalAmount.toFixed(2)}
            </p>
            {cart.cartHeader.couponCode && cart.cartHeader.discountTotal > 0 && (
  <>
    <p className="text-green-600 font-semibold">
      Coupon: {cart.cartHeader.couponCode} (-$
      {cart.cartHeader.discountTotal.toFixed(2)})
    </p>
    <p className="text-xl font-bold">
      Total after discount: ${cart.cartHeader.orderTotal.toFixed(2)}
    </p>
  </>
)}

          </div>
        </>
      )}

      <div className="mt-6">
        <input
          type="text"
          value={coupon}
          onChange={(e) => setCoupon(e.target.value)}
          placeholder="Enter coupon code"
          className="p-2 border rounded w-full mb-2"
        />
        <div className="flex gap-2">
          <button
            onClick={handleApplyCoupon}
            className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
          >
            Apply Coupon
          </button>
          <button
            onClick={handleRemoveCoupon}
            className="bg-gray-500 text-white px-4 py-2 rounded hover:bg-gray-600"
          >
            Remove Coupon
          </button>
        </div>
        {cart.cartDetails.length > 0 && (
  <div className="mt-6 text-right">
    <button
      onClick={() => navigate(`/checkout/${userId}`)}
      className="bg-green-600 text-white px-6 py-2 rounded hover:bg-green-700"
    >
      Checkout
    </button>
  </div>
)}

      </div>
    </div>
  );
};

export default CartPage;

