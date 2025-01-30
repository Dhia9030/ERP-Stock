import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.jsx";
import "./index.css";
import { ProductProvider } from "./context/ProductProvider.jsx";
import { OrderProvider } from "./context/OrderProvider.jsx";
import { NotificationProvider } from "./context/NotificationProvider.jsx";
import { PurchaseProvider } from "./context/PurchaseProvider.jsx";


import { BrowserRouter } from "react-router-dom";

ReactDOM.createRoot(document.getElementById("root")).render(
	<React.StrictMode>
		<BrowserRouter>
		<NotificationProvider>
			<PurchaseProvider>
		<OrderProvider>
			<ProductProvider>
				<App />
			</ProductProvider>
		</OrderProvider>
		</PurchaseProvider>
		</NotificationProvider>
			
		</BrowserRouter>
	</React.StrictMode>
);
