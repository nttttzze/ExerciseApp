import { View, Text, StyleSheet } from "react-native";
import React from "react";

export default function Card(props) {
  return (
    <View style={cardStyle.card}>
      <View style={cardStyle.cardImg}>{props.children}</View>

      <View>
        <Text style={cardStyle.cardTitle}></Text>
      </View>
    </View>
  );
}

const cardStyle = StyleSheet.create({
  card: {
    borderRadius: 6,
    elevation: 3,
    backgroundColor: "#fff",
    shadowOffset: { width: 1, height: 1 },
    shadowColor: "#333",
    shadowOpacity: 0.3,
    shadowRadius: 2,
    marginHorizontal: 4,
    marginVertical: 6,
  },
  cardImg: {},

  cardDesc: {
    marginHorizontal: 18,
    marginVertical: 20,
  },
});
