import { motion } from "framer-motion";

import Header from "../components/common/Header";
import StatCard from "../components/common/StatCard";

import { AlertTriangle, DollarSign, Package, TrendingUp } from "lucide-react";
import CategoryDistributionChart from "../components/overview/CategoryDistributionChart";
import SalesTrendChart from "../components/products/SalesTrendChart";
import ProductsTable from "../components/products/ProductsTable";
import Category from "../components/overview/Category";
import { useState } from "react";

const LowStock = () => {

	const [selectedList, setSelectedList] = useState("all");
	return (
		<div className='flex-1 overflow-auto relative z-10'>
			<Header title='Products' />

			<main className='max-w-7xl mx-auto py-6 px-4 lg:px-8'>
				{/* STATS */}
				

				<ProductsTable selectedList = {selectedList} />

				{/* CHARTS */}
				<div className='grid grid-col-1 lg:grid-cols-2 gap-8'>
				</div>
			</main>
		</div>
	);
};
export default LowStock;
