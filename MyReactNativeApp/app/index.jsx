import {
  View,
  Text,
  StyleSheet,
  ImageBackground,
  Pressable,
} from "react-native";
import React from "react";
import backgroundImg from "@/assets/images/background.jpg";
import { Link } from "expo-router";

const app = () => {
  return (
    <View style={styles.container}>
      <ImageBackground
        source={backgroundImg}
        resizeMode="cover"
        style={styles.image}
      >
        <Text style={styles.title}>Workout App</Text>

        <Link href="/contact" style={{ marginHorizontal: "auto" }} asChild>
          {/* <Pressable style={styles.button}> */}
          {/* <Text style={styles.buttonText}>Contact Us</Text> */}
          {/* </Pressable> */}
        </Link>
      </ImageBackground>
    </View>
  );
};

export default app;

export const styles = StyleSheet.create({
  container: {
    flex: 1,
    flexDirection: "column",
    // justifyContent: "center",
    alignItems: "center",
  },
  image: {
    width: "100%",
    height: "100%",
    flex: 1,
    resizeMode: "cover",
    // justifyContent: "center",
  },
  title: {
    color: "white",
    fontSize: 42,
    fontWeight: "bold",
    textAlign: "center",
    // backgroundColor: "rgba(125, 116, 116, 0.5)",
    color: "black",
    // marginBottom: 580,
    marginTop: 55,
    // position: "fixed",
    zIndex: "2",
  },
  link: {
    color: "white",
    fontSize: 42,
    fontWeight: "bold",
    textAlign: "center",
    textDecorationLine: "underline",
    color: "black",
    // marginBottom: 20,
    padding: 4,
  },
  button: {
    height: 45,
    borderRadius: 20,
    backgroundColor: "rgba(0,0,0,0.75)",
    padding: 6,
    justifyContent: "center",
  },
  buttonText: {
    color: "white",
    fontSize: 16,
    fontWeight: "bold",
    textAlign: "center",
    padding: 4,
  },
  contactContainer: {
    backgroundColor: "#fdfdfdff",
    height: "40%",
    width: "100%",
    justifyContent: "center",
    alignItems: "center",
    top: 200,
    // position: "fixed",
    // marginBottom: 200,
    // flex: 2,
    zIndex: "1",
  },
});
