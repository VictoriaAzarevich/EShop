import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { getCartByUserId } from "../../services/cartService";
import { useParams } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import { Cart } from "../../types/Cart";

const CheckoutPage = () => {
  const { userId } = useParams<{ userId: string }>();
  const { getAccessTokenSilently } = useAuth0();
  const [cart, setCart] = useState<Cart | null>(null);
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phone: "",
    pickupDateTime: "",
    cardNumber: "",
    cvv: "",
    expiryMonthYear: "",
  });

  useEffect(() => {
    const fetchCart = async () => {
      try {
        const token = await getAccessTokenSilently();
        const data = await getCartByUserId(userId!, token);
        setCart(data);
        setForm((prev) => ({
          ...prev,
          ...data.cartHeader,
        }));
      } catch (err) {
        toast.error("Failed to fetch cart");
      }
    };

    if (userId) {
      fetchCart();
    }
  }, [userId, getAccessTokenSilently]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({
      ...form,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      // const token = await getAccessTokenSilently();
      toast.success("Order placed successfully!");
    } catch (err) {
      toast.error("Failed to place order");
    }
  };

  if (!cart) return <div className="p-4">Loading...</div>;

  return (
    <div className="max-w-4xl mx-auto p-4">
      <h2 className="text-2xl font-bold mb-4 text-black-600">Enter details and place order:</h2>
      <form onSubmit={handleSubmit} className="grid grid-cols-1 lg:grid-cols-2 gap-4">
        <div className="space-y-3">
          <input name="firstName" value={form.firstName} onChange={handleChange} placeholder="First Name" className="form-input w-full border p-2 rounded" />
          <input name="lastName" value={form.lastName} onChange={handleChange} placeholder="Last Name" className="form-input w-full border p-2 rounded" />
          <input name="email" value={form.email} onChange={handleChange} placeholder="Email" className="form-input w-full border p-2 rounded" />
          <input name="phone" value={form.phone} onChange={handleChange} placeholder="Phone" className="form-input w-full border p-2 rounded" />
          <input name="pickupDateTime" value={form.pickupDateTime} onChange={handleChange} placeholder="Pickup Date/Time" className="form-input w-full border p-2 rounded" type="datetime-local" />
          <input name="cardNumber" value={form.cardNumber} onChange={handleChange} placeholder="Card Number" className="form-input w-full border p-2 rounded" />
          <input name="cvv" value={form.cvv} onChange={handleChange} placeholder="CVV" className="form-input w-full border p-2 rounded" />
          <input name="expiryMonthYear" value={form.expiryMonthYear} onChange={handleChange} placeholder="MMYY" className="form-input w-full border p-2 rounded" />
        </div>

        <div className="bg-white shadow-md rounded p-4">
          <h3 className="text-lg font-bold mb-2 text-red-500">Product Details</h3>
          <hr className="mb-2" />
          {cart.cartDetails.map((item) => (
            <div key={item.cartDetailsId} className="flex justify-between mb-2">
              <div>{item.product.name}</div>
              <div>${item.product.price.toFixed(2)}</div>
              <div>{item.count}</div>
            </div>
          ))}
          <hr className="my-2" />
          <div className="text-right text-red-600 font-semibold">
            Order Total: ${cart.cartHeader.orderTotal.toFixed(2)}
          </div>
          {cart.cartHeader.discountTotal > 0 && (
            <div className="text-right text-green-600">
              Order Discount: ${cart.cartHeader.discountTotal.toFixed(2)}
            </div>
          )}
          <button type="submit" className="mt-4 w-full bg-green-600 text-white p-2 rounded hover:bg-green-700">
            Place Order
          </button>
        </div>
      </form>
    </div>
  );
};

export default CheckoutPage;
