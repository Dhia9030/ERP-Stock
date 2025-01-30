import { motion } from "framer-motion";
import LowStockTable from "../components/lowstock/LowStockTable";
import Header from "../components/common/Header";



import { useState } from "react";

const LowStock = () => {

	const [selectedList, setSelectedList] = useState("all");
	return (
		<div className='flex-1 overflow-auto relative z-10'>
			<Header title='Products' />

			<main className='max-w-7xl mx-auto py-6 px-4 lg:px-8'>
				{/* STATS */}
				

				<LowStockTable/>

				{/* CHARTS */}
				<div className='grid grid-col-1 lg:grid-cols-2 gap-8'>
				</div>
			</main>
		</div>
	);
};
export default LowStock;
