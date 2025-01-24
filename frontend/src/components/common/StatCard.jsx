import { motion } from "framer-motion";

const StatCard = ({ name, icon: Icon, value, color }) => {
	return (
		<div
			className='min-h-36 min-w-72 bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md overflow-hidden shadow-lg rounded-xl border border-white-700'
			
		>
			<div className='flex flex-col justify-center align-middle px-4 py-5 sm:p-6'>
				<span className='pb-4 flex items-center justify-center  text-2xl font-medium text-white-400'>
					<Icon size={20} className='mr-2' style={{ color }} />
					{name}
				</span>
				<p className='mt-1 text-3xl text-center text-4xl font-semibold text-gray-100'>{value}</p>
			</div>
		</div>
	);
};
export default StatCard;
