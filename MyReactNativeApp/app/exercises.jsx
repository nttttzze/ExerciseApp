import {
  View,
  Text,
  FlatList,
  TouchableOpacity,
  Image,
  Platform,
  ScrollView,
  StyleSheet,
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import backgroundImg from "@/assets/images/background.jpg";
import { styles } from "./index";
import Card from "../components/ui/Card";
import React, { useEffect, useState } from "react";
// import { Exercises } from "../constants/Exercises";
import ExerciseImages from "../constants/ExerciseImages";
// import { ScrollView } from "react-native-reanimated/lib/typescript/Animated";
// import { SafeAreaView } from "react-native-safe-area-context";

function ExercisesPage() {
  const Container = Platform.OS === "web" ? ScrollView : SafeAreaView;
  // const seperatorComponent = <View style={exerciseStyles.seperatorComponent} />;

  const [isLoading, setLoading] = useState(true);
  const [data, setData] = useState([]);

  const getExercises = async () => {
    try {
      const response = await fetch("http://192.168.0.221:5050/api/exercise");
      const json = await response.json();
      setData(json.exercises);
    } catch (error) {
      console.error(error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    getExercises();
  }, []);

  {
    /* <View style={styles.container}>
        <Text style={styles.title}>Exercises</Text>
      </View> */
  }
  return (
    <Container>
      <FlatList
        data={data}
        keyExtractor={(id) => id.toString()}
        showsVerticalScrollIndicator={false}
        contentContainerStyle={{
          alignItems: "center",
          paddingTop: 15,
        }}
        // ItemSeparatorComponent={seperatorComponent}
        ListEmptyComponent={<Text>No exercises</Text>}
        renderItem={({ item }) => (
          <View style={exerciseStyles.row}>
            <View>
              <Text style={exerciseStyles.exerciseName}>
                {item.exerciseName}
              </Text>

              <Text style={exerciseStyles.exerciseTarget}>
                Targeted muscle: {item.mainTargetMuscle}
              </Text>
            </View>
            <Image
              style={exerciseStyles.Image}
              // source={ExerciseImages[item.id - 1]}
              source={{ uri: item.image }}
            ></Image>
          </View>
        )}
      ></FlatList>
    </Container>
    // <Container>
    //   <FlatList
    //     data={Exercises}
    //     keyExtractor={(item) => item.id.toString()}
    //     showsVerticalScrollIndicator={false}
    //     contentContainerStyle={{
    //       alignItems: "center",
    //       paddingTop: 15,
    //     }}
    //     // ItemSeparatorComponent={seperatorComponent}
    //     ListEmptyComponent={<Text>No exercises</Text>}
    //     renderItem={({ item }) => (
    //       <View style={exerciseStyles.row}>
    //         <View>
    //           <Text style={exerciseStyles.exerciseName}>
    //             {item.exerciseName}
    //           </Text>
    //           <Text style={exerciseStyles.exerciseTarget}>
    //             Targeted muscle: {item.mainTargetMuscle}
    //           </Text>
    //         </View>
    //         <Image
    //           style={exerciseStyles.Image}
    //           source={ExerciseImages[item.id - 1]}
    //         ></Image>
    //       </View>
    //     )}
    //   ></FlatList>
    // </Container>
  );
}
export default ExercisesPage;

// function createStyles(theme, colorScheme) {
//   return StyleSheet.create({
//     contentContainer: {
//       paddingTop: 10,
//       paddingBottom: 20,
//       paddingHorizontal: 12,
//     },
//   });
// }

const exerciseStyles = StyleSheet.create({
  contentContainer: {
    paddingTop: 10,
    paddingBottom: 20,
    paddingHorizontal: 12,
  },
  Container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
  },
  Image: {
    width: 120,
    height: 120,
    // paddingLeft: 10,
  },
  seperatorComponent: {
    // height: 1,
    // width: "50%",
    // maxWidth: 300,
    // marginHorizontal: "auto",
    // marginBottom: 10,
    // backgroundColor: "black",
  },
  row: {
    // padding: 2,
    flexDirection: "row",
    width: "100%",
    maxWidth: 600,
    height: 120,
    marginBottom: 10,
    borderStyle: "solid",
    borderColor: "black",
    borderRadius: 10,
    borderWidth: 3,
    overflow: "hidden",
    // marginHorizontal: "auto",
  },
  exerciseName: {
    paddingLeft: 10,
    paddingTop: 5,
    fontSize: 18,
    textDecorationLine: "underline",
    fontWeight: "bold",
  },
  exerciseTarget: {
    width: "65%",
    paddingTop: 10,
    paddingLeft: 10,
    paddingRight: 5,
    flexGrow: 1,
  },
});
