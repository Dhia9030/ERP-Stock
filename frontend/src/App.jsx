import { Route, Routes, useNavigate, Navigate, useLocation } from "react-router-dom";
import Sidebar from "./components/common/Sidebar";
import { OrderProvider } from './context/OrderProvider';
import { ProductProvider, useProducts } from "./context/ProductProvider";
import { useState, useRef, useEffect } from "react";
import AddStockManager from "./pages/AddStockManager";
import OverviewPage from "./pages/OverviewPage";
import Login from "./pages/Login";
import ProductsPage from "./pages/ProductsPage";
import OrdersPage from "./pages/OrdersPage";
import SettingsPage from "./pages/SettingsPage";
import CustomerOrder from "./pages/CustomerOrder";
import LowStock from "./pages/LowStock";
import Transfers from "./pages/Transfers";
import DelayPage from "./pages/DelayPage";
import PurchaseDetails from "./pages/PurchaseDetails";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Purchases from "./pages/Purchases";
import { useNotification } from "./context/NotificationProvider";
import { useContext } from "react";
import { getToken } from './utility/storage';
function App() {
  const navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    const token = getToken();
    if (!token && location.pathname !== '/login') {
      navigate('/login');
    } else if (token && location.pathname === '/login') {
      navigate('/'); // Redirect to home or any other default page if already authenticated
    }
  }, [navigate, location]);

  const { notificationOn } = useNotification();

  const [lowproducts, setLowStockProducts] = useState([]);

  const ClothingMin = 300;
  const ElectronicsMin = 300;
  const FoodMin = 300;

  const PRODUCT_DATA = useProducts();
  useEffect(() => {
    if (PRODUCT_DATA && PRODUCT_DATA.length > 0) {
      const lowStockProducts = PRODUCT_DATA.filter(product => {
        if (product.category === "Clothing") {
          return product.stock < ClothingMin;
        } else if (product.category === "Electronics") {
          return product.stock < ElectronicsMin;
        } else if (product.category === "Food") {
          return product.stock < FoodMin;
        }
        return false;
      });

      setLowStockProducts(lowStockProducts);
    }
  }, [PRODUCT_DATA]);

  const notifiedProducts = useRef(new Set());

  useEffect(() => {
    if (lowproducts && lowproducts.length > 0) {
      lowproducts.forEach((product) => {
        toast.warn(`Low stock: ${product.name}`, {
          position: "top-right",
          autoClose: 1000,
        });
        notifiedProducts.current.add(product.id);
      });
    }
  }, [lowproducts]);

  const isLoginPage = location.pathname === '/login';

  return (
    <div>
      <ToastContainer containerId="login" />
      {!isLoginPage && notificationOn && <ToastContainer />}
      <div className="area"></div>
      <div className='bg-fiber-carbon flex h-screen text-gray-100 overflow-hidden'>
        
        {!isLoginPage && <Sidebar />}
        <Routes>
          <Route path='/' element={<OverviewPage />} />
          <Route path='/products' element={<ProductsPage />} />
          <Route path='/orders' element={<OrdersPage />} />
          <Route path='/orders/:orderId' element={<CustomerOrder />} />
          <Route path='/lowstock' element={<LowStock />} />
          <Route path='/transfers' element={<Transfers />} />
          <Route path='/settings' element={<SettingsPage />} />
          <Route path='/add' element={<AddStockManager />} />
          <Route path='/delay' element={<DelayPage />} />
          <Route path='/purchases' element={<Purchases />} />
          <Route path='/purchases/:purchaseId' element={<PurchaseDetails />} />
          <Route path='/login' element={<Login />} />
          <Route path="/" element={<Navigate to="/login" />} />
        </Routes>
      </div>
    </div>
  );
}

export default App;