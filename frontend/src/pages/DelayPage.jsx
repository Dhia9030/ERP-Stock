import React, { useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import Header from "../components/common/Header";
import DailyOrders from "../components/orders/DailyOrders";
import OrderDistribution from "../components/orders/OrderDistribution";
import OrdersTable from "../components/orders/OrdersTable";
import DelayTable from '../components/delay/DelayTable';



const DelayPage = () => {
    
    

    return (
        <div className='flex-1 relative z-10 overflow-auto'>
            <Header title={"Expiration"} />
            <main className='max-w-7xl mx-auto py-6 px-4 lg:px-8'>
                    <DelayTable  />
                
            </main>
        </div>
    );
};

export default DelayPage;