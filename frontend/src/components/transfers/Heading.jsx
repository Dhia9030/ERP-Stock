import React from "react"
import {motion} from "framer-motion"
import { ArrowUpRight , ArrowDownLeft } from "lucide-react";


const Heading = ({change, title})=>{

    const item = {change : change};
    return(
        <motion.div
					
					className='mt-12 ml-14 mb-12 bg-gray-800 bg-opacity-100 backdrop-filter w-5/12 backdrop-blur-lg shadow-lg
            rounded-xl p-5 border 
          '
			initial={{ opacity: 0, y: 0 }}
			animate={{ opacity: 1, y: 0 }}
			transition={{ duration: 0.5 }}
				>
					<div className='flex items-center justify-between'>
						<div className="flex items-center">
						{item.change > 0 ?  <ArrowDownLeft size='60' color="#90EE90" />: <ArrowUpRight size='60' color="#AFEEEE" />}
							<p className='mt-1 text-3xl font-semibold text-gray-100'>{title}</p>
						</div>

						
					</div>

				</motion.div>
    )
}


export default Heading;