import { Route, Routes } from "react-router-dom";

import Sidebar from "./components/common/Sidebar";
import {OrderProvider} from './OrderProvider';
import { ProductProvider, useProducts } from "./ProductProvider";
import { useState , useRef , useEffect, useMemo} from "react";
import AddAdmin from "./pages/AddAdmin";
import OverviewPage from "./pages/OverviewPage";
import ProductsPage from "./pages/ProductsPage";
import SalesPage from "./pages/SalesPage";
import OrdersPage from "./pages/OrdersPage";
import AnalyticsPage from "./pages/AnalyticsPage";
import SettingsPage from "./pages/SettingsPage";
import CustomerOrder from "./pages/CustomerOrder";
import LowStock  from "./pages/LowStock";
import Transfers from "./pages/Transfers"
import { ToastContainer , toast} from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

//import { BackgroundGradientAnimation } from "./components/ui/background-gradient-animation";


function App() {

	
// Low Stock Notifications
const notifiedProducts = useRef(new Set());

const products = useProducts();
console.log("products:", products); // Debugging log
const memorizedProducts = useMemo(() => products, [products]);
console.log("memorizedProducts:", memorizedProducts); // Debugging log


  useEffect(() => {
    console.log("notifiedProducts (before):", notifiedProducts.current); // Debugging log


    if (memorizedProducts && memorizedProducts.length > 0) {
      memorizedProducts.forEach((product) => {
        if (product.stock <= 5 ) {
          //console.log(`Low stock: ${product.name}`); // Debugging log
          toast.warn(`Low stock: ${product.name}`, {
            position: "top-right",
            autoClose: 5000, // Auto close after 5 seconds
          });
          notifiedProducts.current.add(product.id);
          console.log("notifiedProducts (after):", notifiedProducts.current); // Debugging log

        }
      });

    }
  }, [memorizedProducts]);
  //-----------------------------------------------


  //Orders Notifications

  //-------------------------------------------------




	
	return (

			
			
			

		
		<div>
			<ToastContainer />
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
				<Route path='/sales' element={<SalesPage />} />
				<Route path='/orders' element={<OrdersPage  />} />
				<Route path='/orders/:orderId' element={<CustomerOrder />} />
				<Route path='/lowstock' element={<LowStock />} />
				<Route path='/transfers' element={<Transfers />} />
				<Route path='/analytics' element={<AnalyticsPage />} />
				<Route path='/settings' element={<SettingsPage />} />
				<Route path='/add' element={<AddAdmin />} />
			</Routes>
		</div>
		</div>
		




		
		
	);
}

export default App;
