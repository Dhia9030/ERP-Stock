import { User } from "lucide-react";
import SettingSection from "./SettingSection";
import { jwtDecode } from "jwt-decode";
import { getToken } from "../../utility/storage";
import { useState } from "react";
import { useNavigate } from "react-router-dom";

const Profile = () => {

	const navigate = useNavigate();

	const [token, setToken] = useState(getToken());

	if(!token){
		navigate("/login");
		return null ;
	}

	const decodedToken = jwtDecode(token);
    const { unique_name, email } = decodedToken;

	return (
		<SettingSection icon={User} title={"Profile"}>
			<div className='flex flex-col sm:flex-row items-center mb-6'>
				

				<div>
					<h3 className='text-2xl font-semibold text-gray-100'><span className="text-blue-400">Username : </span>{unique_name}</h3>
					<p className='mt-9 text-xl text-gray-100'><span className="text-2xl text-blue-400 ">Email : </span>{email}</p>
				</div>
			</div>

			
		</SettingSection>
	);
};
export default Profile;
