import { motion } from "framer-motion";
import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from "recharts";
import * as SignalR from '@microsoft/signalr';
import {useEffect} from 'react';

const dailyOrdersData = [
	{ date: "07/01", orders: 45 },
	{ date: "07/02", orders: 52 },
	{ date: "07/03", orders: 49 },
	{ date: "07/04", orders: 60 },
	{ date: "07/05", orders: 55 },
	{ date: "07/06", orders: 58 },
	{ date: "07/07", orders: 62 },
];

const DailyOrders = () => {
	const [dailyOrdersData, setDailyOrdersData] = useState([]);

  useEffect(() => {
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/orderhub")
      .configureLogging(SignalR.LogLevel.Information)
      .build();

    connection.start()
      .then(() => {
        console.log("Connected to SignalR hub for daily orders");

        connection.on("ReceiveDailyOrders", (data) => {
          console.log("Received daily orders data:", data);
          setDailyOrdersData(data);
        });

        connection.invoke("GetDailyOrders")
          .catch(err => console.error(err.toString()));
      })
      .catch(err => console.error("Error connecting to SignalR hub:", err));

    return () => {
      connection.stop().then(() => console.log("Disconnected from SignalR hub"));
    };
  }, []);

	return (
		<motion.div
			className='bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border '
			initial={{ opacity: 0, y: 0 }}
			animate={{ opacity: 1, y: 0 }}
			transition={{ duration: 0.5 }}
		>
			<h2 className='text-xl font-semibold text-gray-100 mb-4'>Daily Orders</h2>

			<div style={{ width: "100%", height: 300 }}>
				<ResponsiveContainer>
					<LineChart data={dailyOrdersData}>
						<CartesianGrid strokeDasharray='3 3' stroke='#374151' />
						<XAxis dataKey='date' stroke='#9CA3AF' />
						<YAxis stroke='#9CA3AF' />
						<Tooltip
							contentStyle={{
								backgroundColor: "rgba(31, 41, 55, 0.8)",
								borderColor: "#4B5563",
							}}
							itemStyle={{ color: "#E5E7EB" }}
						/>
						<Legend />
						<Line type='monotone' dataKey='orders' stroke='#8B5CF6' strokeWidth={2} />
					</LineChart>
				</ResponsiveContainer>
			</div>
		</motion.div>
	);
};
export default DailyOrders;
