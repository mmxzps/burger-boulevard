export const evaluateAdditionalCost = (components, componentTree) =>
  diff(components, componentTree).added.reduce((sum, component) => sum + component.price.current, 0)
  + componentTree.children.reduce((sum, child) => sum + evaluateAdditionalCost(components, child), 0)

export const evaluateCost = (components, componentTree) => {
  let baseCost = components.find(({ id }) => id == componentTree.componentId).price.current
  let additionalCost = evaluateAdditionalCost(components, componentTree)
  return baseCost + additionalCost
}

export const diff = (components, componentTree) =>
  components
    .find(({ id }) => id == componentTree.componentId).childPolicies
    .reduce((diffs, policy) => {
      const childrenByPolicy = componentTree.children.filter(o => o.componentId == policy.child.id)
      const added = childrenByPolicy.length - policy.default

      const changes = Array(Math.abs(added))
        .fill(components.find(({ id }) => id == policy.child.id))

      if (added > 0)
        diffs.added.push(...changes)
      else if (added < 0)
        diffs.removed.push(...changes)

      return diffs
    }, {
      added: [],
      removed: []
    })

export const componentToTreeWithDefaults = (component) => ({
  componentId: component.id,
  children: component.childPolicies.flatMap(policy =>
    Array(policy.default).fill(componentToTreeWithDefaults(policy.child)))
})
