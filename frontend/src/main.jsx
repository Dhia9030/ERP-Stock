import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.jsx";
import "./index.css";
import { ProductProvider } from "./context/ProductProvider.jsx";
import { OrderProvider } from "./context/OrderProvider.jsx";

import { BrowserRouter } from "react-router-dom";

ReactDOM.createRoot(document.getElementById("root")).render(
	<React.StrictMode>
		<BrowserRouter>

		<OrderProvider>
			<ProductProvider>
				<App />
			</ProductProvider>
		</OrderProvider>
			
		</BrowserRouter>
	</React.StrictMode>
);
