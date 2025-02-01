import { Route, Routes } from "react-router-dom";

import Sidebar from "./components/common/Sidebar";
import {OrderProvider} from './context/OrderProvider';
import { ProductProvider, useProducts } from "./context/ProductProvider";
import { useState , useRef , useEffect, useMemo} from "react";
import AddAdmin from "./pages/AddAdmin";
import OverviewPage from "./pages/OverviewPage";
import ProductsPage from "./pages/ProductsPage";
import OrdersPage from "./pages/OrdersPage";
import SettingsPage from "./pages/SettingsPage";
import CustomerOrder from "./pages/CustomerOrder";
import LowStock  from "./pages/LowStock";
import Transfers from "./pages/Transfers"
import DelayPage from "./pages/DelayPage";
import PurchaseDetails from "./pages/PurchaseDetails"
import { ToastContainer , toast} from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import Purchases from "./pages/Purchases";
import { useNotification } from "./context/NotificationProvider";
import { useContext } from "react";

//import { BackgroundGradientAnimation } from "./components/ui/background-gradient-animation";


function App() {

	const {notificationOn} = useNotification();

	const [lowproducts, setLowStockProducts] = useState([]);
	
	const ClothingMin = 300;
	const ElectronicsMin = 300;
	const FoodMin = 300;
	
	const PRODUCT_DATA = useProducts();
	useEffect(() => {
		console.log('PRODUCT_DATA m loul', PRODUCT_DATA);
	
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
	
		console.log('Filtered low stock products:', lowStockProducts);
	
		setLowStockProducts(lowStockProducts);
		}
	}, [PRODUCT_DATA]);

	
// Low Stock Notifications
const notifiedProducts = useRef(new Set());



  useEffect(() => {
    console.log("notifiedProducts (before):", notifiedProducts.current); // Debugging log


    if (lowproducts && lowproducts.length > 0) {
      lowproducts.forEach((product) => {
          //console.log(`Low stock: ${product.name}`); // Debugging log
          toast.warn(`Low stock: ${product.name}`, {
            position: "top-right",
            autoClose: 5000, // Auto close after 5 seconds
          });
          notifiedProducts.current.add(product.id);
          console.log("notifiedProducts (after):", notifiedProducts.current); // Debugging lo
        
      });

    }
  }, [lowproducts]);
  //-----------------------------------------------


  //Orders Notifications

  //-------------------------------------------------




	
	return (

			
			
			

		
		<div>
			{notificationOn && <ToastContainer />}
			<div className="area">
		{/*<ul className="circles">
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
			<li></li>
		</ul>*/}
	</div>
		<div className='bg-fiber-carbon flex h-screen  text-gray-100 overflow-hidden'>
			
			{/* BG */}
			<div className='fixed inset-0 z-0'>
				<div className='absolute inset-0 bg-gradient-to-br from-white-900 via-white-800 to-white-900 opacity-80' />
				<div className='absolute inset-0 backdrop-blur-sm' />
			</div>

			<Sidebar />
			<Routes>
				<Route path='/' element={<OverviewPage />} />
				
				<Route path='/products' element={<ProductsPage />} />
				<Route path='/orders' element={<OrdersPage  />} />
				<Route path='/orders/:orderId' element={<CustomerOrder />} />
				<Route path='/lowstock' element={<LowStock />} />
				<Route path='/transfers' element={<Transfers />} />
				<Route path='/settings' element={<SettingsPage />} />
				<Route path='/add' element={<AddAdmin />} />
				<Route path='/delay' element={<DelayPage />} />
				<Route path='/purchases' element={<Purchases />} />
				<Route path='/purchases/:purchaseId' element={<PurchaseDetails />} />
			</Routes>
		</div>
		</div>
		




		
		
	);
}

export default App;
