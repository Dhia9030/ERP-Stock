import React, { useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import Header from "../components/common/Header";
import PurchasesTable from "../components/purchases/PurchasesTable";
import CustomerOrder from './CustomerOrder';
import {usePurchase} from "../context/PurchaseProvider"


const Purchases = () => {
    
    

    return (
        <div className='flex-1 relative z-10 overflow-auto'>
            <Header title={"Purchases"} />
            <main className='max-w-7xl mx-auto py-6 px-4 lg:px-8'>
                    {<PurchasesTable  />}
             
            </main>
        </div>
    );
};

export default Purchases;