import Heading from "../components/transfers/Heading.jsx"
import Header from "../components/common/Header.jsx"

const Transfers = ()=>{
    return (
        <div className="flex-1 overflow-auto relative z-10">
            <Header title="Transfers"/>
            {/* 0 = sold*/}
            <Heading  change= {0} title="Dispatched Orders
"/> 
            {/* 1 = bought*/}
            <Heading change = {1} title="Product Imports"/>
        </div>


    )
}

export default Transfers