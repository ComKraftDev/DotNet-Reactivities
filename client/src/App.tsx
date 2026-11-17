import { useEffect, useState } from "react";
import axios from "axios";
import { Typography, List, ListItem, ListItemText } from "@mui/material";

function App() {
  const [activities, setActivities] = useState<Activity[]>([]);

  useEffect(() => {
    /* // using fetch API
    fetch("https://localhost:5001/api/activities")
      .then((response) => response.json())
      .then((data) => setActivities(data));
    //.catch((error) => console.error("Error fetching activities:", error));
    */
    // using axios library
    axios
      .get<Activity[]>("https://localhost:5001/api/activities")
      .then((response) => setActivities(response.data));

    return () => {};
  }, []);

  return (
    <>
      <Typography variant="h3">Reactivities</Typography>
      <List>
        {activities.map((activity) => (
          <ListItem key={activity.id}>
            <ListItemText>{activity.title}</ListItemText>
          </ListItem>
        ))}
      </List>
    </>
  );
}

export default App;
