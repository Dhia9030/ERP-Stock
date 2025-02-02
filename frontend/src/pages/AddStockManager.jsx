
import { div } from 'framer-motion/client';
import Form from '../components/addStockManager/Form';
import Header from '../components/common/Header';
const AddStockManager = () => {
    return (
        
        <div className='flex-1 overflow-auto relative z-10'>
            <Header title='Add Stock Manager'/>
            <div className='flex justify-center w-full items-center h-screen bg-gray-100'>
            
                <Form />

            </div>
        
        </div>
    )
}

export default AddStockManager;