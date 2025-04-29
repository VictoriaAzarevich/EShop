import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { Cart } from "../../types/Cart";
import { applyCoupon, getCartByUserId, removeCoupon } from "../../services/cartService";
import { useParams } from "react-router-dom";

const CartPage = () => {
  const { userId } = useParams<{ userId: string }>();
  const [cart, setCart] = useState<Cart | null>(null);
  const [coupon, setCoupon] = useState("");

  const fetchCart = async () => {
    if (!userId) return;

    try {
      const data = await getCartByUserId(userId);
      setCart(data);
    } catch (err) {
      toast.error("Failed to fetch cart");
    }
  };

  useEffect(() => {
    fetchCart();
  }, [userId]);

  const handleRemoveItem = async (cartDetailsId: number) => {
    try {
      await removeItemFromCart(cartDetailsId);
      toast.success("Item removed");
      fetchCart();
    } catch (err) {
      toast.error("Failed to remove item");
    }
  };

  const handleApplyCoupon = async () => {
    try {
      if (!cart) return;
      const updated = { ...cart, cartHeader: { ...cart.cartHeader, couponCode: coupon } };
      await applyCoupon(updated);
      toast.success("Coupon applied");
      fetchCart();
    } catch (err) {
      toast.error("Failed to apply coupon");
    }
  };

  const handleRemoveCoupon = async () => {
    if (!userId) return;
    try {
      await removeCoupon(userId);
      toast.success("Coupon removed");
      fetchCart();
    } catch (err) {
      toast.error("Failed to remove coupon");
    }
  };

  if (!cart) return <div className="p-4">Loading...</div>;

  return (
    <div className="p-4 max-w-2xl mx-auto">
      <h1 className="text-2xl font-bold mb-4">Your Cart</h1>

      {cart.cartDetails.length === 0 ? (
        <p>Your cart is empty.</p>
      ) : (
        <ul className="space-y-4">
          {cart.cartDetails.map((item) => (
            <li key={item.cartDetailsId} className="border p-4 rounded shadow-sm">
              <div className="flex justify-between">
                <div>
                  <p className="font-semibold">{item.product.name}</p>
                  <p>Quantity: {item.count}</p>
                  <p>Price: ${item.product.price}</p>
                </div>
                <button
                  className="text-red-500 hover:underline"
                  onClick={() => handleRemoveItem(item.cartDetailsId)}
                >
                  Remove
                </button>
              </div>
            </li>
          ))}
        </ul>
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
      </div>
    </div>
  );
};

export default CartPage;
function removeItemFromCart(cartDetailsId: number) {
  throw new Error("Function not implemented.");
}

