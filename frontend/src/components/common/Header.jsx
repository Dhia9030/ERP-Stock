//import { useState } from "react";
// import { AnimatePresence, motion } from "framer-motion";
// import { Link } from "react-router-dom";
 import { LogOut } from "lucide-react";
 import { useNavigate } from "react-router-dom";
 import { removeToken } from "../../utility/storage";

// const item = { name: "Log Out", icon: logOut, color: "#8B5CF6", href: "/products" }

const Header = ({ icon ,title }) => {
	const navigate = useNavigate();

	const handleClick = (e)=>{
		e.preventDefault();	
		console.log("clickit");
		removeToken();
		navigate("/login")

	}
	return (
		<header className='flex justify-between items-center bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg border-b rounded-bl-[30px] '>
			<div className='max-w-7xl  py-7 px-4 sm:px-6 lg:px-8 ' >
{/* {			<icon size={20} style={{ color: "green", minWidth: "20px" }} /> */}
				<h1 className='font-montserrat text-2xl font-semibold text-gray-100'>{title}</h1>
			</div>
			<div className='mx-[70px] flex items-center'>
                <button onClick ={handleClick} className='flex items-center p-3 text-sm font-medium text-gray-100 rounded-lg hover:bg-gradient-to-br from-cyan-500 to-cyan-600 transition-colors'>
                    <LogOut size={20} className='mr-2' />
                    Log Out
                </button>
            </div>
		</header>
	);
};
export default Header;
