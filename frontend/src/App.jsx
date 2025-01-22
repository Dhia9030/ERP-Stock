import { Route, Routes } from "react-router-dom";

import Sidebar from "./components/common/Sidebar";
import {OrderProvider} from './OrderProvider';
import { ProductProvider } from "./ProductProvider";
import { useState } from "react";

import OverviewPage from "./pages/OverviewPage";
import ProductsPage from "./pages/ProductsPage";
import SalesPage from "./pages/SalesPage";
import OrdersPage from "./pages/OrdersPage";
import AnalyticsPage from "./pages/AnalyticsPage";
import SettingsPage from "./pages/SettingsPage";
import CustomerOrder from "./pages/CustomerOrder";
import LowStock  from "./pages/LowStock";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import {NotificationProvider} from "./NotificationProvider";

//import { BackgroundGradientAnimation } from "./components/ui/background-gradient-animation";


function App() {
	return (
		
		<OrderProvider>
			<ProductProvider>
			<NotificationProvider>

		
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

				<Route path='/analytics' element={<AnalyticsPage />} />
				<Route path='/settings' element={<SettingsPage />} />
			</Routes>
		</div>
		</div>
		</NotificationProvider>

		</ProductProvider>

		</OrderProvider>
		
		
		
	);
}

export default App;
