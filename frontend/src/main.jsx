import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.jsx";
import "./index.css";
import { ProductProvider } from "./context/ProductProvider.jsx";
import { OrderProvider } from "./context/OrderProvider.jsx";
import { NotificationProvider } from "./context/NotificationProvider.jsx";

import { BrowserRouter } from "react-router-dom";

ReactDOM.createRoot(document.getElementById("root")).render(
	<React.StrictMode>
		<BrowserRouter>
		<NotificationProvider>
		<OrderProvider>
			<ProductProvider>
				<App />
			</ProductProvider>
		</OrderProvider>
		</NotificationProvider>
			
		</BrowserRouter>
	</React.StrictMode>
);
