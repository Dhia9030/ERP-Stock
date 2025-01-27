import Heading from "../components/transfers/Heading.jsx"
import Header from "../components/common/Header.jsx"
import TransferTable from "../components/transfers/TransferTable.jsx"
import InternalTransfer from "../components/transfers/InternalTransfer.jsx"
const Transfers = ()=>{
    return (
        <div className="flex-1 overflow-auto relative z-10">
            <Header title="Transfers"/>
            <InternalTransfer/>

            {/* 0 = sold*/}
            <Heading  change= {0} title="Dispatched Products"/> 
            <TransferTable type={"OUT"}/>

            {/* 1 = bought*/}
            <Heading change = {1} title="Product Imports"/>
            <TransferTable type={"IN"}/>
        </div>


    )
}

export default Transfers