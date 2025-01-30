import { useState } from "react";
import SettingSection from "./SettingSection";
import { Bell } from "lucide-react";
import ToggleSwitch from "./ToggleSwitch";

import { useNotification } from "../../context/NotificationProvider";

const Notifications = () => {

	const{notificationOn , toggleNotification} = useNotification();
	const [notifications, setNotifications] = useState({
		side: true,
		
	});

	const toggleSideNotifications = () => {
		setNotifications({ ...notifications, side: !notifications.side });
		toggleNotification();
	}

	return (
		<SettingSection icon={Bell} title={"Notifications"}>
			<ToggleSwitch
				label={"Side Notifications"}
				isOn={notifications.side}
				onToggle={() => toggleSideNotifications()}
			/>
			
		</SettingSection>
	);
};
export default Notifications;
