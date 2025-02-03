import Heading from "../components/transfers/Heading.jsx"
import Header from "../components/common/Header.jsx"
import TransferTable from "../components/transfers/TransferTable.jsx"
import InternalTransfer from "../components/transfers/InternalTransfer.jsx"
import InternalTransferTable from "../components/transfers/InternalTransferTable.jsx"
import MergedBlocksTable from "../components/transfers/MergedBlocksTable.jsx"
import DeletedBlocksTable from "../components/transfers/DeletedBlocksTable.jsx"
import { useNavigate , useLocation } from "react-router-dom"
import { useEffect, useState } from "react"
import { getToken } from "../utility/storage.jsx"
const Transfers = ()=>{
    const navigate = useNavigate();
      const location = useLocation();
    
      
        const token = getToken();
        if (!token && location.pathname !== '/login') {
          navigate('/login');
          return;
        } 
      
    return (
        <div className="flex-1 overflow-auto relative z-10">
            <Header title="Transfers"/>
            <InternalTransfer/>

            {/* 0 = sold*/}
            <Heading  change= {0} title="Dispatched Blocks"/> 
            <TransferTable type={"OUT"}/>

            {/* 1 = bought*/}
            <Heading change = {1} title="Imported Blocks"/>
            <TransferTable type={"IN"}/>

            {/* -1 = internal*/}
            <Heading change = {-1} title="Internal Transfers"/>
            <InternalTransferTable />
            {/* -2 = merge*/}
            <Heading change = {-2} title="Merged Blocks"/>
            <MergedBlocksTable/>

            {/* -3 = Deleted Blocks*/}
            <Heading change = {-3} title="Deleted Blocks"/>
            <DeletedBlocksTable />
        </div>


    )
}

export default Transfers